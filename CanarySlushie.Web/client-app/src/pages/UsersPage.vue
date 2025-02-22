<script setup lang="ts">
import { ref, computed } from 'vue'
import UserData from '@/data/users.json'

const users = ref(UserData);
const sortedUsers = computed(() => {
  return [...users.value].sort((a, b) => a.Username.localeCompare(b.Username));
});
</script>

<template>
  <div class="text-center p-4">
    <p>A list of TheFinalOutpost users that play ChickenSmoothie.</p>
    <p>Message Koishibuh on Discord to be added to or removed from this list</p>
  </div>

  <div class="grid grid-cols-1 md:grid-cols-3 gap-4 p-4">
    <div v-for="user in sortedUsers" :key="user.Username" class="p-2 bg-gray-100 rounded">
      <a :href="user.Url" class="text-blue-600 underline">{{ user.Username }}</a>
    </div>
  </div>
</template>