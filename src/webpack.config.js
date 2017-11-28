const path = require('path');  
var webpack = require("webpack");

module.exports = {  
    context: __dirname + '/Scripts',
    entry: {
        index: './index.js',
    },
    resolve: {
        extensions: [".js"]
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
            },
        ]
    },
  plugins: [
  new webpack.ProvidePlugin({
    $: 'jquery',
    jQuery: 'jquery',
    'window.jQuery': 'jquery',
    moment: "moment",
    Popper: ['popper.js', 'default']
  })
  ],
    output: {
        filename: '[name].js',
        path: __dirname + '/wwwroot/js/',
    }
};
