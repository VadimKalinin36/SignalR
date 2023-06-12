import request from "@/utils/request";
import { Button, Form, Input, Modal, Select } from "antd";
import React from "react";

export default (props: any) => {
    const [directions, setDirections] = React.useState<[]>();
    const [groups, setGroups] = React.useState<[]>();
    const [countries, setCountries] = React.useState<[]>();


    React.useEffect(() => {

        request('https://localhost:7127/Direction/Index', { method: 'POST', data: {} }).then(result => {
            const dirs = result.map(item => {
                return { value: item.id, label: item.name };
            });
            setDirections(dirs);
        });

        request('https://localhost:7127/Group/Index', { method: 'POST', data: {} }).then(result => {
            const group = result.map(item => {
                return { value: item.id, label: item.name };
            })
            setGroups(group);
        });

        request('https://localhost:7127/Country/Index', { method: 'POST', data: {} }).then(result => {
            const country = result.map(item => {
                return { value: item.id, label: item.name };
            })
            setCountries(country);
        });


    }, []);



    return (
        <>
            <Form.Item name="lastName" label="Фамилия">
                <Input placeholder="Введите фамилию" />
            </Form.Item>
            
            <Form.Item name="firstName" label="Имя">
                <Input placeholder="Введите имя" />
            </Form.Item>
            
            <Form.Item name="middleName" label="Отчество">
                <Input placeholder="Введите отчество" />
            </Form.Item>

            <Form.Item name="groupId" label="Группа" >
                <Select placeholder="Выберите группу"
                    options={groups} />
            </Form.Item>

            <Form.Item name="directionId" label="Направление" >
                <Select placeholder="Выберите направление"
                    options={directions} />
            </Form.Item>

            <Form.Item name="countryId" label="Гражданство" >
                <Select placeholder="Выберите гражданство"
                    options={countries} />
            </Form.Item>

        </>
    );
};
