var mongoose = require('mongoose'),
  env = process.env.NODE_ENV || 'development',
  config = require('../../config/config')[env],
  Schema = mongoose.Schema;

//=============================
// Schema
//=============================
var customerSchema = new Schema({
  CustomerId: String,
  CompanyName: String,
  ContactName: String,
  City: String,
  Country: String
});

module.exports = mongoose.model('Customer', customerSchema);
