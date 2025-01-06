<template>
  <div class="login-container">
    <h2>Login</h2>
    <form @submit.prevent="login">
      <label for="username">Username:</label>
      <input id="username" v-model="username" type="text" />
      
      <label for="password">Password:</label>
      <input id="password" v-model="password" type="password" />

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
  text-align: center;
  margin: 2rem;
}
</style>