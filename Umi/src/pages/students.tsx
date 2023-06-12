import { Link } from "@umijs/max";
import request from "@/utils/request";
import { Button, Form, Input, Select, Space, Table, } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";
import { DeleteFilled, EditFilled, SearchOutlined } from "@ant-design/icons";

const DocsPage = () => {

  const [dataSource, setDataSource] = React.useState([]);
  const [loading, setLoading] = React.useState(false);

  const getStudents = (data: any) => {
    setLoading(true);
    request('https://localhost:7127/Student/Index', { method: 'POST', data }).then(result => {
      console.log(result);
      console.log(data);
      setDataSource(result);
      setLoading(false);
    });
  }

  React.useEffect(() => getStudents({}), []);

  const searchStudentHandler = (data: any) => {
    console.log(data);
    getStudents(data);
  }

  const removeHandler = (id: number) => {

    request(`https://localhost:7127/Student/${id}`, { method: 'DELETE' }).then(result => {
      console.log(result);
      const newDataSource = dataSource.filter((value, index) => value.id != id);
      console.log(newDataSource);
      setDataSource(newDataSource);
    });

  }


  const columns: ColumnsType<never> = [
    {
      title: 'Id',
      dataIndex: 'id',
    },
    {
      title: 'Фамилия',
      dataIndex: 'lastName',
    },
    {
      title: 'Имя',
      dataIndex: 'firstName',
    },
    {
      title: 'Отчество',
      dataIndex: 'middleName',
    },
    {
      title: 'Группа',
      dataIndex: 'group',
      render: (value, record, index) => value.name
    },
    {
      title: 'Направление',
      dataIndex: 'direction',
      render: (value, record, index) => value.name

    },
    {
      title: 'Гражданство',
      dataIndex: 'country',
      render: (value, record, index) => value.name

    },
    {
      title: 'Действия',
      key: 'action',
      render: (value, record, index) =>
        <>
          <Link to={`/edit_student/${record.id}`}><EditFilled /></Link>{' / '}
          <a onClick={() => removeHandler(record.id)}><DeleteFilled /></a>
          
        </>

    }
  ];


  return (
    <div>
      
      <Space direction="vertical" style={{ marginBottom: '10px' }}>
        <Link to="/create_student">
          <Button type="primary">Новая запись</Button>
        </Link>
      </Space>

      <Form onFinish={searchStudentHandler} layout="inline" style={{ marginBottom: '10px' }}>
        <Form.Item name="lastName" style={{ width: '250px' }}>
          <Input allowClear placeholder="Введите фамилию" />
        </Form.Item>

        <Form.Item name="firstName" style={{ width: '250px' }}>
          <Input allowClear placeholder="Введите имя" />
        </Form.Item>

        <Form.Item name="middleName" style={{ width: '250px' }}>
          <Input allowClear placeholder="Введите отчество" />
        </Form.Item>

        <Form.Item name="groupName" style={{ width: '250px' }}>
          <Input allowClear placeholder="Группа" />
        </Form.Item>

        <Form.Item name="directionName" style={{ width: '250px' }}>
          <Input allowClear placeholder="Направление" />
        </Form.Item>

        <Form.Item name="countryName" style={{ width: '250px' }}>
          <Input allowClear placeholder="Гражданство" />
        </Form.Item>

        <Button icon={<SearchOutlined/>} type="primary" htmlType="submit">Искать</Button>

      </Form>

      <Table dataSource={dataSource} columns={columns} loading={loading} />
    </div>
  );
};

export default DocsPage;
