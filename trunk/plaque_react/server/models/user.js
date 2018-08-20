const mongoose 			= require('mongoose');
const bcrypt 			= require('bcrypt');
const bcrypt_p 			= require('bcrypt-promise');
const jwt           	= require('jsonwebtoken');
const validate          = require('mongoose-validator');
const {ThrowError, to}          = require('../services/utilssvc');
const CONFIG            = require('../config/config');

let UserSchema = mongoose.Schema({
    name:  {type:String, unique: true, lowercase:true, trim: true},
    email: {type:String, lowercase:true, trim: true, index: true, unique: true, sparse: true,
            validate:[validate({
                validator: 'isEmail',
                message: 'Not a valid email.',
            })]
    },
    salt: {type:String},
    password: {type:String},
    group: {type:String}
});


UserSchema.pre('save', async function(next){

    if(this.isModified('password') || this.isNew){

        let err, salt, hash;
        [err, salt] = await to(bcrypt.genSalt(10));
        if(err) ThrowError(err.message, true);

        [err, hash] = await to(bcrypt.hash(this.password, salt));
        if(err) ThrowError(err.message, true);

        this.password = hash;

    } else{
        return next();
    }
})

UserSchema.methods.comparePassword = async function(pw){
    let err, pass;
    if(!this.password) ThrowError('password not set');

    [err, pass] = await to(bcrypt_p.compare(pw, this.password));
    if(err) ThrowError(err);

    if(!pass) ThrowError('invalid password');

    return this;
}

UserSchema.methods.toWeb = function(){
    /*
        "user": {
            "_id": "5b6c2ac34e91261c8887f80c",
            "name": "john1",
            "password": "$2b$10$zBFoyLqLvcYUV/TON5DjUekfOvDykaYH149isVWsEti21t2mYQ3BG",
            "group": "chinese",
            "email": "john@eee.com",
            "createdAt": "2018-08-09T11:51:31.583Z",
            "updatedAt": "2018-08-09T11:51:31.583Z",
            "__v": 0,
            "id": "5b6c2ac34e91261c8887f80c"
        }
     */
    var {name, group, email, id} = this;
    var filteduser = {name, group, email, id};
    //let json = JSON.stringify(filteduser);
    return filteduser;
};

let User = module.exports = mongoose.model('User', UserSchema);


