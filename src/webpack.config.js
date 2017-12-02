const path = require('path');
var webpack = require('webpack');

module.exports = {
  context: __dirname + '/Client/js',
  devtool: 'eval-source-map',
  entry: {
    entry: './entry.js',
    exhibitDetails: './ExhibitDetails/exhibitDetails.js',
    exhibitDetailsController: './ExhibitDetails/exhibitDetailsController.js',
    followingService: './ExhibitDetails/followingService.js',

    exhibits: './Exhibits/exhibits.js',
    exhibitsController: './Exhibits/exhibitsController.js',
    attendanceService: './Exhibits/attendanceService.js',

    exhibitCancel: './ExhibitCancel/exhibitCancel.js',
    exhibitCancelController: './ExhibitCancel/exhibitCancelController.js',
    exhibitService: './ExhibitCancel/exhibitService.js',

    notifications: './Notifications/notifications.js',
    notificationsController: './Notifications/notificationsController.js',
    notificationService: './Notifications/notificationService.js'
  },
  resolve: {
    extensions: ['.js', '.css', '.ts'],
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
        exclude: /(node_modules|bower_components)/,
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
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      },
      {
        test: /\.(png|jpg|gif|svg|eot|ttf|woff|woff2)$/,
        loader: 'url-loader',
        options: {
          limit: 10000
        }
      },
      {
        test: /.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
        use: [
          {
            loader: 'file-loader',
            options: {
              name: '[name].[ext]',
              outputPath: 'fonts/',
              publicPath: '../'
            }
          }
        ]
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
    })
  ],
  output: {
    filename: '[name].js',
    path: __dirname + '/wwwroot/js/'
  }
};
