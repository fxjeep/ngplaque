import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { Table, Card, Row, Col, Form, Input, Popconfirm, Icon, Button } from 'antd';
import { getAllContacts, updateContact, deleteContact, addNewContact } from '../Actions/MainPanelActions';

const FormItem = Form.Item;
const EditableContext = React.createContext();

const EditableRow = ({ form, index, ...props }) => (
  <EditableContext.Provider value={form}>
    <tr {...props} />
  </EditableContext.Provider>
);

const EditableFormRow = Form.create()(EditableRow);

class EditableCell extends React.Component {
    getInput = () => {
      return <Input />;
    };
  
    render() {
      const {
        editing,
        dataIndex,
        title,
        inputType,
        record,
        index,
        ...restProps
      } = this.props;
      return (
        <EditableContext.Consumer>
          {(form) => {
            const { getFieldDecorator } = form;
            return (
              <td {...restProps}>
                {editing ? (
                  <FormItem style={{ margin: 0 }}>
                    {getFieldDecorator(dataIndex, {
                      rules: [{
                        required: true,
                        message: `Please Input ${title}!`,
                      }],
                      initialValue: record[dataIndex],
                    })(this.getInput())}
                  </FormItem>
                ) : restProps.children}
              </td>
            );
          }}
        </EditableContext.Consumer>
      );
    }
  }


class MainPanel extends Component {
    constructor(props) {
        super(props);
        this.props.dispatch(getAllContacts());
        this.state = { editingKey: '' };
    }

    contactColumns = [{
        title: 'Name',
        dataIndex: 'name',
        key: 'name',
        editable: true
      },
      {
        title: 'Code',
        dataIndex: 'code',
        key: 'code',
        editable: true
      },
      {
        title: 'Last Print',
        dataIndex: 'lastPrint',
        key: 'lastPrint',
      },
      {
        title: 'operation',
        dataIndex: 'operation',
        render: (text, record) => {
            const editable = this.isEditing(record);
            return (
              <div>
                {editable ? (
                    <span>
                    <EditableContext.Consumer>
                        {form => (
                            <a
                                href="javascript:;"
                                onClick={() => this.save(form, record.id)}
                                className="controlIcon"
                            ><Icon type="check" /></a>
                        )}
                    </EditableContext.Consumer>
                    
                        <a
                            href="javascript:;"
                            onClick={() => this.cancel(record.id)}
                            className="controlIcon"
                        ><Icon type="close" /></a>
                    </span>
                ) : (
                  <div>
                    <a onClick={() => this.edit(record.id)} className="controlIcon"><Icon type="edit" /></a>
                    <Popconfirm
                      title="Sure to delete?"
                      onConfirm={() => this.delete(record.id)}
                    >
                      <a className="controlIcon"><Icon type="close-circle-o" /></a>
                    </Popconfirm>

                    <a onClick={() => this.editDetails(record.id)} className="controlIcon"><Icon type="contacts" /></a>
                  </div>
                )}
              </div>
            );
          },
        },
    ];

    isEditing = (record) => {
        return record.key === this.state.editingKey;
    };

    edit(key) {
        this.setState({ editingKey: key });
    };

    addNew(){
      this.props.dispatch(addNewContact());
    };

    editDetails(key){

    }

    cancel = () => {
        this.setState({ editingKey: '' });
    };

    delete(key) {
      this.props.dispatch(deleteContact(key));
    }


    save(form, key) {
      form.validateFields((error, row) => {
          const index = this.props.contacts.findIndex(item => key === item.key);
          if (index > -1) {
            const item = this.props.contacts[index];
            this.props.contacts.splice(index, 1, {
              ...item,
              ...row,
            });
            this.props.dispatch(updateContact(this.props.contacts[index]));
            this.setState({ editingKey: '' });
          }
      });
    }

    tablComponents = {
        body: {
          row: EditableFormRow,
          cell: EditableCell,
        },
    };

    render(){

        const columns = this.contactColumns.map((col) => {
            if (!col.editable) {
              return col;
            }
            return {
              ...col,
              onCell: record => ({
                record,
                inputType: 'text',
                dataIndex: col.dataIndex,
                title: col.title,
                editing: this.isEditing(record),
              }),
            };
          });

        return <div className="mainpanel">
            <Card title="Contacts" bordered={true}>
                <Row type="flex">
                    <Col><Button type="primary" onClick={()=>this.addNew()}>Add Contact</Button></Col>
                    <Col><Button type="primary" >Gen Print File</Button></Col>
                    <Col><Button type="primary" >Export Data</Button></Col>
                </Row>
                <Row type="flex">
                    <Table 
                        rowKey='id'
                        components={this.tablComponents}
                        columns={columns} 
                        dataSource={this.props.contacts} />
                </Row>
            </Card>
        </div>
    }
}


function mapStateToProps(state) {
    const { contacts } = state.mainPanel;
    return {
        contacts
    };
  }
  
const connectedMainPanel = connect(mapStateToProps)(MainPanel);
export { connectedMainPanel as MainPanel };