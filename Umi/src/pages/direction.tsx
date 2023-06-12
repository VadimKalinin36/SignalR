import { Link } from "@umijs/max";
import request from "@/utils/request";
import { Button, Form, Input, Space, Table, } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";
import { EditFilled, DeleteFilled, SearchOutlined } from "@ant-design/icons";

const DocsPage = () => {

  const [dataSource, setDataSource] = React.useState([]);
  const [loading, setLoading] = React.useState(false);

  const getDirections = (data: any) => {
    setLoading(true);
    request('https://localhost:7127/Direction/Index', { method: 'POST', data }).then(result => {
      console.log(result);
      console.log(data);
      setDataSource(result);
      setLoading(false);
    });
  }

  React.useEffect(() => getDirections({}), []);

  const searchDirectionHandler = (data: any) => {
    console.log(data);
    getDirections(data);
  }

  const removeHandler = (id: number) => {

    request(`https://localhost:7127/Direction/${id}`, { method: 'DELETE' }).then(result => {
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
      sorter: (a, b) => a.id - b.id,
    },
    {
      title: 'Название',
      dataIndex: 'name',
    },
    {
      title: 'Действия',
      key: 'action',
      render: (value, record, index) =>
        <>
          <Link to={`/edit_direction/${record.id}`}><EditFilled /></Link>{' / '}
          <a onClick={() => removeHandler(record.id)}><DeleteFilled /></a>
        </>

    }
  ];


  return (
    <div>
      <Space direction="vertical" style={{ marginBottom: '10px' }}>
        <Link to="/create_direction">
          <Button type="primary">Новое направление</Button>
        </Link>
      </Space>

      <Form onFinish={searchDirectionHandler} layout="inline" style={{ marginBottom: '10px' }}>
        <Form.Item name="name" style={{ width: '250px' }}>
          <Input allowClear placeholder="Введите название направления" />
        </Form.Item>

        <Button icon={<SearchOutlined/>} type="primary" htmlType="submit">Искать</Button>

      </Form>

      <Table dataSource={dataSource} columns={columns} loading={loading} />
    </div>
  );
};

export default DocsPage;
