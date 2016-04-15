module.exports = {
  development: {
    rootPath: require('path').normalize(__dirname + '/..'),
    db: 'mongodb://192.168.99.100:27017/local'
  },
  staging: {
    rootPath: require('path').normalize(__dirname + '/..'),
    db: 'mongodb://my-mongodb/local'
  },
};
