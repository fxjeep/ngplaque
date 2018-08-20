import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Row, Col, Button } from 'antd';
import { userActions } from '../Actions';

class LoginOut extends Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }

    logout(e){
        this.props.dispatch(userActions.logout());
    }

    render(){
        return <div className="LoginOut">
            <Row>
                <Col span={2}>
                   {this.props.loggedIn?<Button onClick={this.logout}>Log out</Button>:null}
                </Col>
            </Row>
            </div>
    }
}

function mapStateToProps(state) {
    const { loggedIn, error } = state.authentication;
    return {
        loggedIn,
        error
    };
  }
  
const connectedLoginOut = connect(mapStateToProps)(LoginOut);
export { connectedLoginOut as LoginOut };