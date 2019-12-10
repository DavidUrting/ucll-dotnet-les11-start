module.exports = {
    entry:
    {
        shared: './src/shared.js',
        home: './src/home/home.js',
        customer: './src/sales/customer/customer.js'
    },
    output: {
        filename: '../wwwroot/js/[name].js'
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/](jquery)[\\/]|[\\/]node_modules[\\/](jquery-validation)[\\/]|[\\/]node_modules[\\/](jquery-validation-unobtrusive)[\\/]/,
                    name: 'vendor',
                    chunks: 'all'
                }
            }
        }
    }
};
