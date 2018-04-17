const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');

const UglifyWebpackPlugin = require("uglifyjs-webpack-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const cssnano = require("cssnano");
const CompressionPlugin = require("compression-webpack-plugin");


module.exports = {
  context: __dirname + '/Assets',
  devtool: 'eval-source-map',
  entry: {
    main: './main.js',
    attendances: './js/Attendance/attendances.js',
    followings: './js/Following/followings.js',
    exhibitCancel: './js/ExhibitCancel/exhibitCancel.js',
  },
  resolve: {
    extensions: ['.js', '.scss', '.css', '.ts'],
    modules: [
      path.resolve('./'),
      path.resolve('./node_modules')
    ],
  },
  module: {
    rules: [{
        test: /\.exec\.js$/,
        use: ['script-loader']
      },
      {
        test: /\.js$/,
        exclude: /(node_modules)/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env']
          }
        }
      },
      {
        test: /\.tsx?$/,
        loader: 'ts-loader'
      },
      {
        test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
        loader: 'url-loader?limit=10000'
      },
      {
        test: /\.(jpg|png|svg)$/,
        use: {
          loader: 'url-loader',
          options: {
            limit: 25000
          }
        }
      },
      {
        test: /\.css$/,
        use: ExtractTextPlugin.extract({
          fallback: 'style-loader',
          use: 'css-loader'
        })
      },
      {
        test: /\.(scss)$/,
        use: ExtractTextPlugin.extract({
          fallback: 'style-loader',
          use: [{
              loader: 'css-loader'
            },
            {
              loader: 'postcss-loader',
              options: {
                plugins: function() {
                  return [require('precss'), require(
                    'autoprefixer')];
                }
              }
            },
            {
              loader: 'sass-loader'
            }
          ]
        })
      }
    ]
  },
  plugins: [
    new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
    new ExtractTextPlugin('styles.css'),
    new CleanWebpackPlugin(['wwwroot']),
    new UglifyWebpackPlugin(),
    new OptimizeCSSAssetsPlugin(),
    new CopyWebpackPlugin([{
      from: 'images',
      to: 'images'
    }])
  ],
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'wwwroot')
  }
};
