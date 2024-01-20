import { useState} from "react";
import { Button, Modal } from 'antd';

const ShowOrdersModal = ({ order }) => {
  const [visible, setVisible] = useState(false);
  const [orderState, setOrderState] = useState(null);

  const showModal = () => {
    setVisible(true);
    setOrderState(order);
  };

  const handleModalExit = () => {
    setVisible(false);
    setOrderState(null);
  };

  return (
    <>
      {order && (
        <>
          <Button type="link" onClick={showModal}>
            Просмотр
          </Button>
          <Modal
            key={orderState?.id}
            title={`ID Заказа: ${orderState?.id}`}
            open={visible}
            onOk={handleModalExit}
            onCancel={handleModalExit}
          >
            <p><b>Ожидает отправления:</b> {orderState?.from}</p>
            <p><b>Будет доставлено:</b> {orderState?.to}</p>
            <p><b>Вес заказа:</b> {orderState?.weight} кг</p>
            <p><b>Отправка:</b> {orderState?.pickupDate}</p>
          </Modal>
        </>
      )}
    </>
  );
};

export default ShowOrdersModal;
