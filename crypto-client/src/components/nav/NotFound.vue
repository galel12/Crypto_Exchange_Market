<template>
    <div class="not-found-container">
        <h1 class="not-found-title">404 - Page Not Found</h1>
        <p class="not-found-message">Oops! The page you are looking for does not exist.</p>
        <button class="redirect-button" @click="goToLogin">Go to Login</button>
    </div>
</template>

<script>
import { useRouter } from "vue-router";

export default {
    name: "NotFound",
    setup() {
        const router = useRouter();

        const goToLogin = () => {
            router.push("/"); // Redirects to the login page
        };

        return {
            goToLogin,
        };
    },
};
</script>

<style>
.not-found-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-color: #f4f4f9;
    color: #333;
    text-align: center;
    padding: 40px;
    border-radius: 20px;
}

.not-found-title {
    font-size: 48px;
    font-weight: bold;
    margin-bottom: 20px;
}

.not-found-message {
    font-size: 18px;
    margin-bottom: 30px;
}

.redirect-button {
    padding: 10px 20px;
    background-color: #1877f2;
    color: white;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.redirect-button:hover {
    background-color: #145db2;
}
</style>