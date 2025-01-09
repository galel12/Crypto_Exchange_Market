<template>
  <div class="signup-container">
    <h2>Sign Up</h2>
    <form @submit.prevent="signUp">
      <p>
        <label for="username">Username:</label>
        <input id="username" v-model="username" type="text" />
      </p>
      <p>
        <label for="password">Password:</label>
        <input id="password" v-model="password" type="password" />
      </p>
      <button type="submit">Sign Up</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "SignUp",
  data() {
    return {
      username: "",
      password: "",
    };
  },
  methods: {
    async signUp() {
      try {
        const response = await fetch("http://localhost:5040/api/User", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ username: this.username, password: this.password }),
        });
        if (!response.ok) throw new Error("Sign Up failed");
        const data = await response.json();
        console.log("Sign Up successful:", data);
      } catch (error) {
        console.error(error);
      }
    },
  },
});
</script>

<style scoped>
.signup-container {
  width: 400px;
  margin: auto;
  padding: 30px;
  border-radius: 20px;
  background: white;
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.3);
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