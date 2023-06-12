import { Button, Form, Input, Modal, Select } from "antd";
import React from "react";

export default (props: any) => {



    return (
        <>
            <Form.Item name="name" label="Название страны">
                <Input placeholder="Введите название страны"/>
            </Form.Item>

        </>
    );
};
