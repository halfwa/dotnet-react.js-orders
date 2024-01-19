import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Space, Table, Button, Modal, Flex } from 'antd';


const API_URL = "https://localhost:7257/api";

const OrdersMainForm = () => {

  const [data, setData] = useState([]);
  const [modalStates, setModalStates] = useState([]);
  const [selectedOrders, setSelectedOrders] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(API_URL + "/orders");
        setData(response.data);
        setModalStates(response.data.map(() => false));
        setSelectedOrders(response.data.map(() => null));
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const showModal = (index, order) => {
    const updatedModalStates = [...modalStates];
    updatedModalStates[index] = true;
    setModalStates(updatedModalStates);

    const updatedSelectedOrders = [...selectedOrders];
    updatedSelectedOrders[index] = order;
    setSelectedOrders(updatedSelectedOrders);
  };

  const handleEscape = (index) => {
    const updatedModalStates = [...modalStates];
    updatedModalStates[index] = false;
    setModalStates(updatedModalStates);

    const updatedSelectedOrders = [...selectedOrders];
    updatedSelectedOrders[index] = null;
    setSelectedOrders(updatedSelectedOrders);
  }

    
  const handleCreateOrder = () => 
    window.location.href = window.location.origin + '/orders/create'; 

  const columns = [
    {
      title: 'Уникальный идентификатор',
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
      render: (_, record, index) => (
        <Space size="middle">
          <Button type="primary" onClick={() => showModal(index, record)}>
            Показать
          </Button>
          <Modal
            title={`Order ID ${record.id}`}
            open={modalStates[index]}
            onOk={() => handleEscape(index)}
            onCancel={() => handleEscape(index)}
          >
            <p><b>Ожидает отправления:</b> {record.from}</p>
            <p><b>Будет доставленно:</b> {record.to}</p>
            <p><b>Вес заказа:</b> {record.weight} кг</p>
            <p><b>Отправка:</b> {record.pickupDate}</p>
          </Modal>
        </Space>
      ),
      key: 'action',
    },
  ];

  return (
    <Flex
    gap="middle" vertical={true}
    >
      <Flex>
          <Table
            pagination={{ pageSize: 8 }}
            columns={columns}
            dataSource={data.map((item) => ({
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
      </Flex>
      <Flex gap="small" wrap="wrap">
        <Button 
        type="primary"
        onClick={() => handleCreateOrder()}
        >
          Создать новый заказ</Button> 
      </Flex>
   </Flex>
  );
  };

export default OrdersMainForm;
