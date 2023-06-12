import FormGroupEdit from "@/components/FormGroupEdit";
import ModalComponent from "@/components/ModalComponent";
import { Link } from "@umijs/max";
import { useParams, history } from "@umijs/max";
import request from "@/utils/request";
import { Button, Form, Input, message, Spin, Table } from "antd";
import { ColumnsType } from "antd/es/table";
import React from "react";

const DocsPage = (props: any) => {
  const params = useParams();
  const [messageApi, contextHolder] = message.useMessage();

  const [data, setData] = React.useState();


  React.useEffect(() => {
    request(`https://localhost:7127/Group/${params.id}`).then(result => {
      console.log(result);
      setData(result);
      // form.setFieldsValue(result);
    });
  }, []);

  const editHandler = (data: any) => {
    console.log(data)

    request(`https://localhost:7127/Group/${params.id}`, { method: 'POST', data }).then(result => {
      history.push('/docs');
      message.success("Данные сохранены")
    });

  }

  const [form] = Form.useForm();

  return (
    <>

      {data ? <Form onFinish={editHandler} form={form} initialValues={data}>
        <Form.Item name="id" hidden></Form.Item>
        <FormGroupEdit />
        <Button type="primary" htmlType="submit">Сохранить</Button>

      </Form> : <Spin />}



    </>
  );
};

export default DocsPage;
