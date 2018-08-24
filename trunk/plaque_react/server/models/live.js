const mongoose = require('mongoose');


let LiveSchema = mongoose.Schema({
    contactId :  {type:String, trim: true},
    name:  {type:String, trim: true},
    lastPrint: {type:String, trim: true}
});

module.exports = mongoose.model('Deceased', DeceasedSchema);
