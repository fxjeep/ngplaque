import React, { Component } from 'react';
import {
  Router,
  Redirect,
  Route,
  Link
} from 'react-router-dom';
import { Layout, Menu, Breadcrumb } from 'antd';

import { history } from './Utils/history';
import { Login } from './Components/Login';
import { MainPanel}  from './Components/MainPanel';
import { EditDetail } from './Components/EditDetail';
import {LoginOut} from './LoginOut';


import 'antd/dist/antd.css';

class App extends Component {
  render() {
    return (
      <Layout className="layout">
    <Layout.Header>
      <div className="logo" />
      <Menu
        theme="dark"
        mode="horizontal"
        defaultSelectedKeys={['2']}
        style={{ lineHeight: '64px' }}
      >
        <LoginOut/>
      </Menu>
    </Layout.Header>
    <Layout.Content style={{ padding: '0 50px' }}>
      
      <Router history={history}>
      <div style={{ background: '#fff', padding: 24, minHeight: 280 }}>
        <Route exact path="/" component={Login}/>
        <Route exact path="/main" render={(props)=>{
                    if (true){
                        return <MainPanel {...props} />
                    }else{
                        return <Redirect to="/" />
                    }
                }} />
        <Route exact path="/detail" render={(props)=>{
              if (true){
                  return <EditDetail {...props} />
              }else{
                  return <Redirect to="/" />
              }
          }} />
      </div>
      </Router>
      
    </Layout.Content>
    <Layout.Footer style={{ textAlign: 'center' }}>
      Ant Design ©2016 Created by Ant UED
    </Layout.Footer>
  </Layout>
    );
  }
}




export default App;
