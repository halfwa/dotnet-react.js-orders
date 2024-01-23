import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {Table, Button, Flex, Typography } from 'antd';
import ShowOrdersModal from './ShowOrdersModal';
import { getAllOrders } from '../../services/orders';

const OrdersMainForm = () => {

  const navigate = useNavigate();
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    const getOrders = async () => {
      const orders = await getAllOrders();
      setOrders(orders);
    };

    getOrders();
  }, []);


  const title = (<Typography.Title level={1}>Заказы на доставку</Typography.Title>)
  const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id'
    },
    {
      title: 'Место отправления',
      dataIndex: 'from',
      key: 'from'
    },
    {
      title: 'Место назначения',
      dataIndex: 'to',
      key: 'to',
    },
    {
      title: 'Вес заказа, кг',
      dataIndex: 'weight',
      key: 'weight',
    },
    {
      title: 'Дата отправки',
      dataIndex: 'pickupDate',
      key: 'pickupDate',
    },
    {
      render: (record) => showModal(record),
      key: 'action'
    }
  ];

  const showModal = (record) => {
    return (
      <ShowOrdersModal order={record} />
    )
  };

  return (
    <div style={{ maxWidth: '55%', margin: 'auto', textAlign: 'center' }}>
      <Flex
      gap="middle" vertical={true}>
            <Table
              title={() => title}
              pagination={{ pageSize: 8 }}
              columns={columns}
              dataSource={orders.map((item) => ({
                id: item.id,
                from: item.fromCity + ", " + item.fromAddress,
                to: item.toCity + ", " + item.toAddress,
                weight: item.weight,
                pickupDate: new Intl.DateTimeFormat('ru-RU', {
                  year: 'numeric',
                  month: 'numeric',
                  day: 'numeric',
                  hour: 'numeric',
                  minute: 'numeric',
                  second: 'numeric',
                  hour12: false, // Формат 24-часов
                }).format(new Date(item.pickupDate)),
              }))}
            />
        <Flex gap="small" wrap="wrap">
          <Button 
          style={{ marginLeft: 'auto'}}
          type="primary"
          onClick={() => navigate('/orders')}
          >
            Создать новый заказ</Button> 
        </Flex>
    </Flex>
   </div>
  );
  };

export default OrdersMainForm;
