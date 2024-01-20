import './App.css';
import { BrowserRouter as Router, Routes, Route} from "react-router-dom"
import { Navigate } from'react-router-dom';
import OrdersMainForm from './components/orders/OrdersMainForm';
import CreateOrdersForm from './components/orders/CreateOrdersForm';

function App() {
  return (  
    <Router>
        <Routes>
          <Route path="/" element={ <Navigate to="/orders" replace />} />
          <Route path="/orders" element={ <OrdersMainForm/> }/>
          <Route path="/orders/create" element={ <CreateOrdersForm/> }/>
        </Routes>
    </Router>
  );
}

export default App;
  