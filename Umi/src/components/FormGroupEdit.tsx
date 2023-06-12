import { Button, Form, Input, Modal, Select } from "antd";
import React from "react";

export default (props: any) => {



    return (
        <>
            <Form.Item name="name" label="Название группы">
                <Input placeholder="Введите название группы"/>
            </Form.Item>
            <Form.Item name="type" label="Тип" >
                <Select placeholder="Выберите тип группы"
                    options={[
                        { value: 1, label: 'Bachelor' },
                        { value: 2, label: 'Magister' },
                        { value: 3, label: 'GraduateStudent' },
                        { value: 4, label: 'Specialist' },
                    ]} />




            </Form.Item>
        </>
    );
};
