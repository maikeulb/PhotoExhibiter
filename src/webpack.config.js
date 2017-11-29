const path = require('path');
var webpack = require('webpack');

module.exports = {
  context: __dirname + '/Client',
  devtool: 'eval-source-map',
  entry: {
    index: './js/index.js'
  },
  resolve: {
    extensions: ['.js', '.css', '.ts']
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/
      },
      {
        test: /\.tsx?$/,
        loader: 'ts-loader'
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      },
      {
        test: /\.(png|jpg|gif|svg|eot|ttf|woff|woff2)$/,
        loader: 'url-loader',
        options: {
          limit: 10000
        }
      }
    ]
  },
  plugins: [
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery',
      'window.jQuery': 'jquery',
      Popper: ['popper.js', 'default']
    })
  ],

  output: {
    filename: '[name].js',
    path: __dirname + '/wwwroot/js/'
  }
};
