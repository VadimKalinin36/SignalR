
import request from "@/utils/request";
import { useModel } from "@umijs/max";
import { Button, Form, Input, Popconfirm, Select, Space, Table, Typography, } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";

const DocsPage = () => {

  const { name, setName } = useModel("useUserModel");


  const formNextHandler = () => {

  }


  return (
    <div>
      <div>Вы ввели имя {name}</div>
      <Form onFinish={formNextHandler} layout="inline" style={{ marginBottom: '10px' }}>
        <Form.Item name="name" style={{ width: '230px' }}>
          <Input allowClear placeholder="E-mail" />
        </Form.Item>
        <Button type="primary" htmlType="submit">Далее</Button>

      </Form>

    </div>
  );
};

export default DocsPage;
