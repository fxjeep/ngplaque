import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { Table, Card, Row, Col, Form, Input, Popconfirm } from 'antd';
import { getAllContacts, updateContact } from '../Actions/MainPanelActions';

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
                            onClick={() => this.save(form, record.key)}
                            style={{ marginRight: 8 }}
                        >Save</a>
                        )}
                    </EditableContext.Consumer>
                    
                        <a
                            href="javascript:;"
                            onClick={() => this.cancel(record.key)}
                        >Cancel</a>
                    </span>
                ) : (
                    <a onClick={() => this.edit(record.key)}>Edit</a>
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

    cancel = () => {
        this.setState({ editingKey: '' });
    };

    save(form, key) {
          const index = this.props.contacts.findIndex(item => key === item.key);
          if (index > -1) {
            const item = this.props.contacts[index];
            this.props.dispatch(updateContact(item));
          }
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
                    <Col>Commands</Col>
                </Row>
                <Row type="flex">
                    <Table 
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