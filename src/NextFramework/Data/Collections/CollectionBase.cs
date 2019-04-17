using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using NextFramework.Data.Entities;
using NextFramework.Helpers;
using NextFramework.Native;

namespace NextFramework.Data.Collections
{
    internal abstract class CollectionBase<T> : IPool<T>, IInternalCollection where T : IEntity
    {
        protected readonly IntPtr _nativePointer;

        private readonly ConcurrentDictionary<IntPtr, T> _entities = new ConcurrentDictionary<IntPtr, T>();

        public int Count => _entities.Count;

        public T this[int index] => _entities.Values.FirstOrDefault(x => x.Id == index);

        public T this[uint index] => this[(int) index];

        internal T this[IntPtr index]
        {
            get
            {
                if (_entities.TryGetValue(index, out T entity) == false)
                {
                    return default(T);
                }

                return entity;
            }
        }

        internal CollectionBase(IntPtr nativePointer)
        {
            _nativePointer = nativePointer;
        }

        protected abstract T BuildEntity(IntPtr entityPointer);

        public T GetAt(int index)
        {
            return GetAt((uint) index);
        }

        public T GetAt(uint index)
        {
            var pointer = Rage.Pool.Pool_GetAt(_nativePointer, index);

            if (_entities.TryGetValue(pointer, out T entity) == false)
            {
                return default(T);
            }

            return entity;
        }

        public async Task<IReadOnlyCollection<T>> GetInRangeAsync(Vector3 position, float range, uint dimension)
        {
            IntPtr entityPointers = IntPtr.Zero;
            ulong size = 0;

            await TickScheduler.Instance
                .Schedule(() => Rage.Pool.Pool_GetInRange(_nativePointer, position, range, dimension, out entityPointers, out size))
                .ConfigureAwait(false);

            return ArrayHelper.ConvertFromIntPtr(entityPointers, size, x => this[x]);
        }

        public async Task<IReadOnlyCollection<T>> GetInDimensionAsync(uint dimension)
        {
            IntPtr entityPointers = IntPtr.Zero;
            ulong size = 0;

            await TickScheduler.Instance
                .Schedule(() => Rage.Pool.Pool_GetInDimension(_nativePointer, dimension, out entityPointers, out size))
                .ConfigureAwait(false);

            return ArrayHelper.ConvertFromIntPtr(entityPointers, size, x => this[x]);
        }

        public IEntity GetEntity(IntPtr entity)
        {
            return this[entity];
        }

        public bool RemoveEntity(IntPtr entityPointer, out IEntity entity)
        {
            if (!_entities.TryRemove(entityPointer, out var removed))
            {
                entity = null;
                return false;
            }

            entity = removed;

            if (removed is Entity internalEntity)
            {
                internalEntity.Exists = false;
            }

            return true;
        }

        public bool CreateAndSaveEntity(IntPtr entityPointer, out IEntity entity)
        {
            entity = CreateAndSaveEntity(entityPointer);

            return entity != null;
        }

        protected T CreateAndSaveEntity(IntPtr entityPointer)
        {
            return _entities.GetOrAdd(entityPointer, BuildEntity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _entities.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
