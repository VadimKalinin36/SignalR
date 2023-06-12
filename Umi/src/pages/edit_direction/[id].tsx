import FormDirectionEdit from "@/components/FormDirectionEdit";
import { useParams, history } from "@umijs/max";
import request from "@/utils/request";
import { Button, Form, message, Spin, } from "antd";
import React from "react";

const DocsPage = (props: any) => {
  const params = useParams();
  const [messageApi, contextHolder] = message.useMessage();

  const [data, setData] = React.useState();


  React.useEffect(() => {
    request(`https://localhost:7127/Direction/${params.id}`).then(result => {
      console.log(result);
      setData(result);
    });
  }, []);

  const editHandler = (data: any) => {
    console.log(data)

    request(`https://localhost:7127/Direction/${params.id}`, { method: 'POST', data }).then(result => {
      history.push('/direction');
      message.success("Данные сохранены")
    });

  }

  const [form] = Form.useForm();

  return (
    <>

      {data ? <Form onFinish={editHandler} form={form} initialValues={data}>
        <Form.Item name="id" hidden></Form.Item>
        <FormDirectionEdit />
        <Button type="primary" htmlType="submit">Сохранить</Button>

      </Form> : <Spin />}



    </>
  );
};

export default DocsPage;
