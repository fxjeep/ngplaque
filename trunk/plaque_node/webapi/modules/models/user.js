let mongoose = require('mongoose');
let Schema = mongoose.Schema;

let UserSchema = new Schema({
    isActive: {type: Boolean, default: true},
    dateCreated: {type: Date, default: Date.now},
    email: String,
    password:String
});
UserSchema.methods.toJSON = function () {
    let obj = this.toObject();
    return obj
};

UserSchema.virtual('id')
    .get(function () {
        return this._id;
    });

UserSchema.methods.validate = function(pwd) {
    if (pwd === this.password) return true;
    return false;
};


module.exports.UserModel = mongoose.model('adminUser', UserSchema, 'adminUsers');