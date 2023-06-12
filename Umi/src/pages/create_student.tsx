import { Button, Form } from "antd";
import { history } from "@umijs/max";
import request from "@/utils/request";
import FormStudentEdit from "@/components/FormStudentEdit";
import React from "react";



const DocsPage = (props: any) => {



  const createHandler = (data: any) => {
    console.log(data)

    request(`https://localhost:7127/Student/`, { method: 'PUT', data }).then(result => {
        history.push('/students');

    });

  }


  return (
    <>

      <Form onFinish={createHandler}>

        <FormStudentEdit />

        <Button type="primary" htmlType="submit">Создать запись</Button>
      </Form>

    </>
  );
};

export default DocsPage;
