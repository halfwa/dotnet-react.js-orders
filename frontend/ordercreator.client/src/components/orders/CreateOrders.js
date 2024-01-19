import axios from "axios";
import { useEffect, useState } from "react";
import CreateOrderForm from "./CreateOrdersForm";
import OrdersList from "./OrdersList";

const API_URL = "https://localhost:7257/api/orders/";

function CreateOrders() {
const [id, setId] = useState("");
const [stname, setName] = useState("");
const [course, setCourse] = useState("");
const [students, setUsers] = useState([]);
 
  useEffect(() => {
    (async () => await Load())();
  }, []);
 
  async function Load() {
    const result = await axios.get(API_URL);
    setUsers(result.data);
  }
 
    async function add(event) {
      event.preventDefault();
      try {
        await axios.post(API_URL, {
          
          stname: stname,
          course: course,
        
        });
        alert("Student Registation Successfully");
            setId("");
            setName("");
            setCourse("");
          
        Load();
      } catch (err) {
        alert(err);
      }
    }

  async function showOrder(students) {
    setName(students.stname);
    setCourse(students.course);
    setId(students.id);
  }

    return (
      <div>

        <h1>Orders</h1>

      <div class="container mt-4">
      < CreateOrderForm/>
      </div>

      <div class="container mt-4">
      < OrdersList/>
      </div>
        
      </div>
    );
  }
  
  export default CreateOrders;