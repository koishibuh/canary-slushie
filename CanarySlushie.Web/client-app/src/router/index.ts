import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '@/pages/HomePage.vue';
import ItemsPage from '@/pages/ItemsPage.vue';
import UsersPage from '@/pages/UsersPage.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePage
    },
    {
      path: '/items',
      name: 'items',
      component: ItemsPage
    },
    {
      path: '/users',
      name: 'users',
      component: UsersPage
    }
  ]
});

export default router;