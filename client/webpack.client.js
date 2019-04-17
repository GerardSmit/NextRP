module.exports = {
    resolve: { 
        extensions: [ '.tsx', '.ts', '.js' ]
    },
    entry: {
        'client_packages': './src/client',
    },
    output: {
        path: 'C:\\RAGEMP\\server-files\\client_packages\\next',
        filename: 'index.js'
    },
    target: 'node',
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: '/node_modules/'
            }
        ]
    }
};