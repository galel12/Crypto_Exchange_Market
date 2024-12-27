<template>
  <div id="app" class="app-container">
    <!-- Header -->
    <header class="app-header">
      <h1 class="header-title">Crypto Exchange Marketplace</h1>
      <nav class="navigation">
        <router-link to="/">Login</router-link>
        <router-link to="/signup">Sign Up</router-link>
        <router-link to="/wallet">Wallet</router-link>
      </nav>
    </header>

    <!-- Main Content -->
    <main class="main-content">
      <router-view />
    </main>
  </div>
</template>

<script>
export default {
  name: "App",
};
</script>

<style scoped>
/* App Container */
.app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: #f5f5f5;
  color: #333;
  font-family: Arial, sans-serif;
}

/* Header */
.app-header {
  background-color: #2a2a72;
  color: #fff;
  padding: 1rem;
  text-align: center;
}

.header-title {
  margin: 0;
  font-size: 2rem;
}

.navigation {
  margin-top: 0.5rem;
}

.navigation a {
  color: #fff;
  margin: 0 1rem;
  text-decoration: none;
  font-weight: bold;
}

.navigation a:hover {
  text-decoration: underline;
}

/* Main Content */
.main-content {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 2rem;
}
</style>