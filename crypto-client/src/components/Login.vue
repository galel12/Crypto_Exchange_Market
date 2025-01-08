<template>
  <div class="login-container">
    <h2>Login</h2>
    <form @submit.prevent="login">
      <p>
        <label for="username">Username:</label>
        <input id="username" v-model="username" type="text" />
      </p>

      <p>
        <label for="password">Password:</label>
        <input id="password" v-model="password" type="password" />
      </p>
      <button type="submit">Login</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "Login",
  data() {
    return {
      username: "",
      password: "",
    };
  },
  methods: {
    async login() {
      try {
        const response = await fetch("http://localhost:5040/api/Auth", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ username: this.username, password: this.password }),
        });
        if (!response.ok) throw new Error("Login failed");
        const data = await response.json();
        console.log("Login successful:", data);
      } catch (error) {
        console.error(error);
      }
    },
  },
});
</script>

<style scoped>
.login-container {
  width: 400px;
  margin: auto;
  padding: 30px;
  border-radius: 20px;
  background: white;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

h2 {
  text-align: center;
  margin-bottom: 20px;
  color: #333;
  font-size: 32px; /* Increased size */
  font-weight: bold;
}

form p {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
  color: #555;
  font-size: 16px; /* Increased size */
  font-weight: bold;
}

input {
  width: 100%;
  padding: 12px;
  border: 1px solid #1877f2;
  border-radius: 5px;
  box-sizing: border-box;
  font-size: 18px;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #000000;
  color: white;
  border: none;
  border-radius: 10px;
  font-size: 20px;
  cursor: pointer;
  margin-top: 15px;
}

button:hover {
  background-color: #41454b;
}
</style>