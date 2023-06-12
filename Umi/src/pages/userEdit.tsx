
import request from "@/utils/request";
import { useModel, history } from "@umijs/max";
import { Button, Form, Input, Popconfirm, Select, Space, Table, Typography, } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";

const DocsPage = () => {

  // useUserModel

  const { name, setName } = useModel("useUserModel");


  const formNextHandler = (data: any) => {
    setName(data.name);
    history.push('/userEdit2');
  }


  return (
    <div>


      <Form onFinish={formNextHandler} layout="inline" style={{ marginBottom: '10px' }}>
        <Form.Item name="name" style = {{width: '230px'}}>
          <Input allowClear placeholder="Логин" />
        </Form.Item>

        <Button type="primary" htmlType="submit">Далее</Button>

      </Form>

    </div>
  );
};

export default DocsPage;
