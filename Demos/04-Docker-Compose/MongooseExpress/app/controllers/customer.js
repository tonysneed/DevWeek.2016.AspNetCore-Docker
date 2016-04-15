var Customer = require('../models/customer.js');

//=============================
// List
//=============================
exports.index = function(req, res) {
  Customer.find(function(err, customers) {
    if (err) throw err;

    res.send(customers);
  });
};

//=============================
// Show
//=============================
exports.show = function(req, res) {
  var id = req.params.id;

  Customer.findOne({ CustomerId: id }, function(err, customer) {
    if (err) throw err;
    if (customer === null)
      res.send(404);

    res.send(customer);
  });
};

//=============================
// Create
//=============================
exports.create = function(req, res) {
  new Customer(req.body).save(function(err, customer) {
    if (err) throw err;

    res.send(customer);
  });
};

//=============================
// Update
//=============================
exports.update = function(req, res) {
  var id = req.params.id;

  Customer.findOneAndUpdate({ CustomerId: id }, req.body, function(err, customer) {
    if (err) throw err;
    if (customer === null)
      res.send(404);

    res.send(customer);
  });
};

//=============================
// Delete
//=============================
exports.delete = function(req, res) {
  var id = req.params.id;

  Customer.remove({ CustomerId: id }, function (err) {
    if (err) throw err;

    res.send(200);
  });
};
