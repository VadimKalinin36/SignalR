import FormGroupEdit from "@/components/FormGroupEdit";
import ModalComponent from "@/components/ModalComponent";
import request from "@/utils/request";
import { Link } from "@umijs/max";
import { useParams, history } from "@umijs/max";
import { Button, Form, Input, message, Spin, Table } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";

const DocsPage = (props: any) => {
  const [messageApi, contextHolder] = message.useMessage();

  const createHandler = (data: any) => {
    console.log(data)

    request(`https://localhost:7127/Group/`, { method: 'PUT', data }).then(result => {
      history.push('/docs');
      //messageApi.success("Данные сохранены")
    });

  }


  return (
    <>

      <Form onFinish={createHandler}>

        <FormGroupEdit />

        <Button type="primary" htmlType="submit">Создать запись</Button>
      </Form>

    </>
  );
};

export default DocsPage;
