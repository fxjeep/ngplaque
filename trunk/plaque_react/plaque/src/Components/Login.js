import React, { Component } from 'react';
import { Button, Select, Input, Card, Row, Col, Alert } from 'antd';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import request, {get, post} from '../Utils/request';
import { userActions } from '../Actions';

const Option = Select.Option;

class Login extends Component {
    loginUrl = "http://localhost:4000/login";

    constructor(props) {
        super(props);
        this.state = {
            group:'Chinese',
            username:'',
            password:''
        }

        this.changeGroup = this.changeGroup.bind(this);
        this.updateUsername = this.updateUsername.bind(this);
        this.updatePassword = this.updatePassword.bind(this);
        this.login = this.login.bind(this);

    }

    changeGroup(value) {
        this.setState({group:value});
    }

    updateUsername(event){
        this.setState({username:event.target.value});
    }

    updatePassword(event){
        this.setState({password:event.target.value});
    }

    login(e){
        e.preventDefault();
    
        let username = this.state.username;
        let password = this.state.password;
        const { dispatch } = this.props;
        if ( username && password ) {
           dispatch(userActions.login(username, password));
        }
    }

    render(){
        return <div className="Login">
            <Card title="Login" bordered={true}>
            <Row type="flex">
                <Col span={2}>Select Group:</Col>
                <Col>
                    <Select defaultValue="Chinese"  value={this.state.group} onChange={this.changeGroup}>
                        <Option value="Chinese">Chinese</Option>
                        <Option value="Vietnamese">Vietnamese</Option>
                    </Select>
                </Col>
            </Row>
            <Row type="flex">
                <Col span={2} >User Name:</Col>
                <Col >
                    <Input addonBefore="" defaultValue="" name="username" id="username"
                    value={this.state.username} onChange={this.updateUsername}/>
                </Col>
            </Row>
            <Row type="flex">
                <Col span={2} >Password:</Col>
                <Col >
                    <Input addonBefore="" defaultValue="" type="password" name="password" id="password"
                    value={this.state.password} onChange={this.updatePassword}/>
                </Col>
            </Row>
            <Row type="flex">
                {this.props.error!=''?<Col span={5}><Alert message={this.props.error} type="error" showIcon></Alert>
                </Col>:null}
            </Row>
            <Row type="flex">
                <Col span={2}>
                    <Button type="primary" onClick={this.login}>Login</Button>
                </Col>
                
            </Row>
        </Card>
      </div>
    }
}

function mapStateToProps(state) {
    const { loggingIn,error } = state.authentication;
    return {
        loggingIn,
        error
    };
  }
  
const connectedLogin = connect(mapStateToProps)(Login);
export { connectedLogin as Login };
