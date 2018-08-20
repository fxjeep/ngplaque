const User = require('../models/user');
const validator = require('validator');
var jwtsimple = require('jwt-simple');
const jwt = require('jsonwebtoken');
const config = require("../config/config");
const { to, ThrowError }    = require('../services/utilssvc');

const createUser = async function(userInfo){
        let  err, userReturn;
        [err, userReturn] = await to(User.create(userInfo));
        if(err) ThrowError('user already exists with that email');
        return userReturn;    
}
module.exports.createUser = createUser;

const authUser = async function(userInfo){//returns token
    let err, userReturn;
    [err, userReturn] = await to(User.findOne({name:userInfo.name}));

    if(err) ThrowError(err.message);
    if(!userReturn) ThrowError('Not registered');

    [err, userReturn] = await to(userReturn.comparePassword(userInfo.password));

    if(err) ThrowError(err.message);

    return userReturn;

}
module.exports.authUser = authUser;

const getJWT = function(userid){
    let expiration_time = parseInt(config.JWT_EXPIRATION);
    return "Bearer "+jwt.sign({user_id:userid}, config.JWT_ENCRYPTION, {expiresIn: expiration_time});
};
module.exports.getJWT = getJWT;

const checkJWT = function(token){
    var payload = null;
    try {
      payload = jwtsimple.decode(token, config.JWT_ENCRYPTION);
      return payload;
    }
    catch (err) {
      return "";
    }
};
module.exports.checkJWT = checkJWT;