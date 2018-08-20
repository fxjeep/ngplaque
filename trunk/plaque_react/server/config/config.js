module.exports = {
    MONGO_URI: process.env.MONGO_URI || 'mongodb+srv://linkspeed:link1234@plaque-nxu5c.mongodb.net/test?retryWrites=true',
    //TOKEN_SECRET: process.env.TOKEN_SECRET || 'pvpnCCZfwOF85pBjbOebZiYIDhZ3w9LZrKwBZ7152K89mPCOHtbRlmr5Z91ci4L',
    LISTEN_PORT: process.env.LISTEN_PORT || 4000,
    JWT_ENCRYPTION : process.env.JWT_ENCRYPTION || '5Kb8kLf9zgWQnogidDA76Mz_SAMPLE_PRIVATE_KEY_DO_NOT_IMPORT_PL6TsZZY36hWXMssSzNydYXYB9KF',
    JWT_EXPIRATION : process.env.JWT_EXPIRATION || '10000'
};