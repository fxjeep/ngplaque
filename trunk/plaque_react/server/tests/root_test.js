var request = require('supertest');
var server = require('../app');
describe('test root request', function () {
    it('responds to root', function testSlash(done) {
        request(server)
            .get('/')
            .expect(200)
            .end(function(err, res) {
                if (res.body.status === "success")
                    done();
            });
    });

    it('signup', function testSlash(done) {
        request(server)
            .post('/auth/signup')
            .send({name: 'john', password:'john', email:"aaa@aaa.com", group:"chinese"})
            .expect(200)
            .end(function(err, res) {
                if (res.body.status === "success")
                    done();
            })
    });

    it('login', function testSlash(done) {
        request(server)
            .post('/auth/login')
            .send({name: 'john1', password:'1234'})
            .expect(200)
            .end(function(err, res) {
                if (res.body.token !== "")
                    done();
            })
    });
});