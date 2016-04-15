var env = process.env.NODE_ENV || 'development',
  config = require('./config')[env];

module.exports = function (app) {
  //=============================
  // Static
  //=============================
  app.get('/', function(req, res) {
    res.sendfile('/index.html', { root: config.rootPath });
  });

  //=============================
  // Bookmark
  //=============================
  var customer = require('../app/controllers/customer');
  app.get('/customer/', customer.index);
  app.get('/customer/:id', customer.show);
  app.post('/customer/', customer.create);
  app.put('/customer/:id', customer.update);
  app.del('/customer/:id', customer.delete);
};
