var uristring = 'mongodb://linkspeed:link1234@plaque-shard-00-00-nxu5c.mongodb.net:27017,'+
'plaque-shard-00-01-nxu5c.mongodb.net:27017,'+
'plaque-shard-00-02-nxu5c.mongodb.net:27017'+
'/plaque?ssl=true&replicaSet=plaque-shard-0&authSource=admin';
var port = process.env.PORT || 8001;

module.exports = {
    MONGO_CONNECT_URL:uristring,
    PORT : port
};