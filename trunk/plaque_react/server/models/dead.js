const mongoose = require('mongoose');


let DeceasedSchema = mongoose.Schema({
    contact :  {type:String, trim: true},
    deadname:  {type:String, trim: true},
    livename:  {type:String, trim: true},
    relation:  {type:String, trim: true},
    lastPrint: {type:String, trim: true}
});

module.exports = mongoose.model('Deceased', DeceasedSchema);
