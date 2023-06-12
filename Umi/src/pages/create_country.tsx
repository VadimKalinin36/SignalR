import request from "@/utils/request";
import { Button, Form, message } from "antd";
import { useParams, history } from "@umijs/max";
import FormCountryEdit from "@/components/FormCountryEdit";


const DocsPage = (props: any) => {

  const createHandler = (data: any) => {
    console.log(data)

    request(`https://localhost:7127/Country/`, { method: 'PUT', data }).then(result => {
        history.push('/country');
        message.success("Данные сохранены")

    });

  }


  return (
    <>

      <Form onFinish={createHandler}>

        <FormCountryEdit />

        <Button type="primary" htmlType="submit">Создать запись</Button>
      </Form>

    </>
  );
};

export default DocsPage;
