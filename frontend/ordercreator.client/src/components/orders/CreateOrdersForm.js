import React, { useState } from 'react';
import axios from 'axios';
import {
  Button,
  DatePicker,
  Form,
  Input,
  InputNumber,
  Col,
  Row,
  Slider,
  Typography
} from 'antd';


const API_URL = "https://localhost:7257/api";

const formItemLayout = {
  labelCol: {
    xs: {
      span: 24,
    },
    sm: {
      span: 6,
    },
  },
  wrapperCol: {
    xs: {
      span: 24,
    },
    sm: {
      span: 14,
    },
  },
};

const DecimalStep = ({ setWeight }) => {
  const [inputValue, setInputValue] = useState(0);

  const onChange = (value) => {
    console.log('Slider value:', value);
    if (isNaN(value)) {
      return;
    }
    setInputValue(value);
    setWeight(value); 
  };

  return (
    <Row>
      <Col span={12}>
        <Slider
          min={0}
          max={1000}
          onChange={onChange}
          value={typeof inputValue === 'number' ? inputValue : 0}
          step={0.1}
        />
      </Col>
      <Col span={4}>
        <InputNumber
          min={0.001}
          max={1000}
          style={{
            margin: '0 16px',
          }}
          step={0.01}
          value={inputValue}
          onChange={onChange}
        />
      </Col>
    </Row>
  );
};

async function createOrder(event, order) {
  event.preventDefault();
  try {

    await axios.post(API_URL, order);
    window.location.href = window.location.origin + '/orders'; 
  } catch (err) {

    alert(err);
  }
}  

const CreateOrdersForm = () => 
{
  const [weight, setWeight] = useState(0);

  const handleSubmit = (values) => {

    const order = {
      fromCity: values.FromCity,
      fromAddress: values.FromAddress,
      toCity: values.ToCity,
      toAddress: values.ToAddress,
      weight: weight, 
      pickupDate: values.PickupDate,
    };
 
    createOrder(order);
  };
  return (
  <Form
    onFinish={handleSubmit}
    {...formItemLayout}
    variant="filled"
    style={{
      maxWidth: 600,
    }}
  >
    <Typography.Title level={3}>Создание заказа</Typography.Title>

    <div className="startPoint">
      <Typography.Title level={5}>Место отправления</Typography.Title>
      <Form.Item
        label="Город"
        name="FromCity"
        rules={[
          {
            required: true,
            message: 'Введите название города',
          },
        ]}
        hasFeedbacks 
      >
        <Input placeholder="Город отправления"/>
      </Form.Item>

      <Form.Item
        label="Адрес"
        name="FromAddress"
        rules={[
          {
            required: true,
            message: 'Введите адрес отправления',
          },
        ]}
      >
        <Input placeholder="Адрес отправления"/>
      </Form.Item>
    </div>

    <div className="dstPoint">
      <Typography.Title level={5}>Место назначения</Typography.Title>
      <Form.Item
        label="Город"
        name="ToCity"
        rules={[
          {
            required: true,
            message: 'Введите название города',
          },
        ]}
      >
        <Input placeholder="Город назначения"/>
      </Form.Item>

      <Form.Item
        label="Адрес"
        name="ToAddress"
        rules={[
          {
            required: true,
            message: 'Введите адрес назначения',
          },
        ]}
      >
        <Input placeholder="Адрес назначения"/>
      </Form.Item>
    </div>

  
    <Form.Item
        label="Вес заказа"
        name="Weight"
        rules={[
          {
            required: true,
            message: 'Укажите вес заказа',
          },
        ]}
      >
        <DecimalStep setWeight={setWeight} />
      </Form.Item>
    <Form.Item
      label="Дата забора"
      name="PickupDate" 
      rules={[
        {
          required: true,
          message: 'Укажите дату забора', 
        },
      ]}
    >
      <DatePicker />
    </Form.Item>

    <Form.Item
      wrapperCol={{
        offset: 6,
        span: 16,
      }}
    >
      <Button type="primary" htmlType="submit">
        Создать заказ 
      </Button>
    </Form.Item>
  </Form>
  );
}

export default CreateOrdersForm;

