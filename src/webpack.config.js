const path = require('path');
var webpack = require('webpack');

module.exports = {
  context: __dirname + '/Client/js',
  devtool: 'eval-source-map',
  entry: {
    _layout: './_layout.js',

    exhibitDetails: './ExhibitDetail/exhibitDetail.exec.js',
    exhibitDetailsController:
      './ExhibitDetail/exhibitDetailsController.exec.js',
    followingService: './ExhibitDetail/followingService.exec.js',
    exhibits: './Exhibits/exhibits.exec.js',
    exhibitsController: './Exhibits/exhibitsController.exec.js',
    attendanceService: './Exhibits/attendanceService.exec.js'
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
