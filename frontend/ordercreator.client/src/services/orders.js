//const API_URL = "http://localhost:80/api";

export const getAllOrders = async () => {
    const response = await fetch('api/orders', {
        method: 'GET'
    });
    if (response.ok) {
        return await response.json();
    } else {
        throw new Error(`Failed to fetch orders. Status: ${response.status}`);
    }
}

export const createOrder = async (orderRequest) => {
    const response = await fetch('api/orders', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orderRequest)  
    });

    if (!response.ok) {
        throw new Error(`Failed to create order. Status: ${response.status}`);
    }
}

export const updateOrder = async (id, orderRequest) => {
    const response = await fetch(`api/orders/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orderRequest)  
    });

    if (!response.ok) {
        throw new Error(`Failed to update order. Status: ${response.status}`);
    }
}

export const deleteOrder = async (id) => {
    const response = await fetch(`api/orders/${id}`, {
        method: 'DELETE'
    });

    if (!response.ok) {
        throw new Error(`Failed to delete order. Status: ${response.status}`);
    }
}
