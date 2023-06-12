import { Link } from "@umijs/max";
import request from "@/utils/request";
import { Button, Form, Input, Space, Table, message, } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";
import { DeleteFilled, EditFilled, SearchOutlined } from "@ant-design/icons";

const DocsPage = () => {

  const [dataSource, setDataSource] = React.useState([]);
  const [loading, setLoading] = React.useState(false);

  const getCountries = (data: any) => {
    setLoading(true);
    request('https://localhost:7127/Country/Index', { method: 'POST', data }).then(result => {
      console.log(result);
      console.log(data);
      setDataSource(result);
      setLoading(false);
    });
  }

  React.useEffect(() => getCountries({}), []);

  const searchCountryHandler = (data: any) => {
    console.log(data);
    getCountries(data);
  }

  const removeHandler = (id: number) => {

    request(`https://localhost:7127/Country/${id}`, { method: 'DELETE' }).then(result => {
      console.log(result);
      const newDataSource = dataSource.filter((value, index) => value.id != id);
      console.log(newDataSource);
      setDataSource(newDataSource);
      message.success('Запись удалена!')
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
          <Link to={`/edit_country/${record.id}`}><EditFilled /></Link>{' / '}
          <a onClick={() => removeHandler(record.id)}><DeleteFilled /></a>
        </>
    }
  ];


  return (
    <div>
      <Space direction="vertical" style={{ marginBottom: '10px' }}>
        <Link to="/create_country">
          <Button type="primary">Добавить страну</Button>
        </Link>
      </Space>

      <Form onFinish={searchCountryHandler} layout="inline" style={{ marginBottom: '10px' }}>
        <Form.Item name="name" style={{ width: '250px' }}>
          <Input allowClear placeholder="Введите название страны" />
        </Form.Item>

        <Button icon={<SearchOutlined/>} type="primary" htmlType="submit">Искать</Button>

      </Form>

      <Table dataSource={dataSource} columns={columns} loading={loading} />
    </div>
  );
};

export default DocsPage;
