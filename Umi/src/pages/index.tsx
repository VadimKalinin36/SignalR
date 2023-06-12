import ModalComponent from '@/components/ModalComponent';
import { Line } from '@ant-design/charts';
import { useModel } from '@umijs/max';
import { Button, Modal } from 'antd';
import React from 'react';


export default function HomePage() {

  const { initialState } = useModel("@@initialState");


  const modalCallback = () => {
    console.log("Modal callback")
  };

  


  return (
    <div>
      <h2>Welcome, {initialState?.name}! </h2>
      <p>

        {/* <ModalComponent buttonText="Test Modal" callback={modalCallback} /> */}

      </p>
      <div>

      </div>
    </div>
  );
}
