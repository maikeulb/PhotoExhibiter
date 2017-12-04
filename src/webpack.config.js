const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');

module.exports = {
  context: __dirname + '/ClientApp',
  devtool: 'eval-source-map',
  entry: {
    main: './main.js',
    exhibitDetails: './js/ExhibitDetails/exhibitDetails.js',
    exhibits: './js/Exhibits/exhibits.js',
    exhibitCancel: './js/ExhibitCancel/exhibitCancel.js',
    notifications: './js/Notifications/notifications.js'
  },
  resolve: {
    extensions: ['.js', '.scss', '.css', '.ts'],
    modules: [path.resolve('./'), path.resolve('./node_modules')]
  },
  module: {
    rules: [
      {
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
          use: [
            {
              loader: 'css-loader'
            },
            {
              loader: 'postcss-loader',
              options: {
                plugins: function() {
                  return [require('precss'), require('autoprefixer')];
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
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery',
      'window.jQuery': 'jquery',
      Popper: ['popper.js', 'default']
    }),
    new webpack.optimize.CommonsChunkPlugin({
      name: 'node-static',
      filename: 'node-static.js',
      minChunks(module, count) {
        var context = module.context;
        return context && context.indexOf('node_modules') >= 0;
      }
    }),
    new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
    new ExtractTextPlugin('styles.css'),
    new CleanWebpackPlugin(['wwwroot']),
    new CopyWebpackPlugin([{ from: 'images', to: 'images' }])
  ],
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'wwwroot')
  }
};
