import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, DatePicker, Form, Input, InputNumber, Typography, Space, notification } from 'antd';
import { createOrder } from '../../services/orders';


const CreateOrdersForm = () => {
  const[loading, setLoading] = useState(false);
  const navigate = useNavigate();

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
  
  const handleCreateOrder = async (orderRequest) => {
    try {
      await createOrder(orderRequest);
      notification.success({
        message: 'Подтверждение',
        description: 'Заказ успешно создан!',
      });
      navigate('/');
    } catch (err) {
      notification.error({
        message: 'Ошибка',
        description: 'Не удалось создать заказ. Пожалуйста, попробуйте позже',
      });
    } finally {
      setLoading(false);
    }
  }  

  const onFinish = async (values) => {
    setLoading(true)
    const orderRequest = {
      fromCity: values.FromCity,
      fromAddress: values.FromAddress,
      toCity: values.ToCity,
      toAddress: values.ToAddress,
      weight: values.Weight, 
      pickupDate: values.PickupDate,
    };
 
    await handleCreateOrder(orderRequest);
  };

  return (
    <>
      {
        loading ? <div>loading...</div>
        :
        <div style={{ maxWidth: 600, margin: 'auto', textAlign: 'center', position: 'relative' }}>
          <Form
            onFinish={(onFinish)}
            {...formItemLayout}
            variant="filled"
            style={{
              maxWidth: 600,
            }}
          >
            <Button
            name='BackButton'
            style={{ position: 'absolute', left: 0, top: '1%' }}
            type='primary'
            onClick={() => navigate('/')}
            >
              Назад
              </Button>
          
            <Space size="middle">
              <Typography.Title level={3}>Оформление заказа</Typography.Title>
            </Space>

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
          
            <Form.Item
                label="Вес заказа (кг)"
                name="Weight"
                rules={[
                  {
                    required: true,
                    message: 'Укажите вес заказа',
                  },
                ]}
              >
                <InputNumber
                    min={0.001}
                    max={1000}
                    style={{
                      margin: '0 16px',
                    }}
                    step={0.01}
                />
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
                span: 14,
              }}
            >
              <Button 
              style={{ marginLeft: 'auto', backgroundColor: 'rgb(31, 136, 61)' }}
              type= 'primary' 
              htmlType= 'submit'>
                Создать
              </Button>
            </Form.Item>
          </Form>
        </div>
      }
    </>
  );
}

export default CreateOrdersForm;

