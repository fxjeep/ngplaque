import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { editDetailActions } from '../Actions/EditDetailsActions';
import { Table, Card, Row, Col, Form, Input, Popconfirm, Icon, Button, Tabs } from 'antd';

import request, {get, post} from '../Utils/request';
import { userActions } from '../Actions';

class EditDetail extends Component {
    constructor(props) {
        super(props);
        var contactId = 1;
        this.props.dispatch(editDetailActions.getDetail(contactId));
        this.state = {        };
    }

    render(){
        return <div>
            <Card title="Edit Details" bordered={true}>
                <Row type="flex">
                    <Col><Button type="primary"><Icon type="plus-circle-o" /></Button></Col>
                    <Col><Button ><Icon type="printer" /></Button></Col>
                    <Col><Button type="primary" ><Icon type="export" /></Button></Col>
                </Row>
                <Tabs type="card">
                    <Tabs.TabPane tab="Live" key="1">Live</Tabs.TabPane>
                    <Tabs.TabPane tab="Deceased" key="2">Deceased</Tabs.TabPane>
                    <Tabs.TabPane tab="Ancestor" key="3">Ancestor</Tabs.TabPane>
                </Tabs>
            </Card>
        </div>

    }
}

function mapStateToProps(state) {
    const { editDetails } = state.editDetails;
    return {
        editDetails
    };
  }
  
const connectedEditDetail = connect(mapStateToProps)(EditDetail);
export { connectedEditDetail as EditDetail };