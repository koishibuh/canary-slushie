<script setup lang="ts">
import { ref, watch } from 'vue';
import itemsJson from '@/data/items.json';

const pageNumbers = ref<number[]>([]);
const itemsPerPage = ref(9);
const currentPage = ref<number>(1);
const indexOfLastItem = ref();
const indexOfFirstItem = ref();
const currentItems = ref();
const displayItems = ref(false);
const filteredItems = ref();
const filteredItemCount = ref(0);

const search = ref('');
const paginateNumber = ref(0);

function searchFor() {
  displayError.value = false;
  displayItems.value = false;

  filteredItems.value = itemsJson.flatMap((item) => {
    return item.itemList
      .filter((subItem) => subItem.name.toLowerCase().includes(search.value.toLowerCase()))
      .map((subItem) => {
        return {
          archivePage: item.archivePage,
          image: subItem.image,
          name: subItem.name
        };
      });
  });

  filteredItemCount.value = filteredItems.value.length;
  indexOfLastItem.value = currentPage.value * itemsPerPage.value;
  indexOfFirstItem.value = indexOfLastItem.value - itemsPerPage.value;
  currentItems.value = filteredItems.value.slice(indexOfFirstItem.value, indexOfLastItem.value);

  paginateNumber.value = Math.ceil(filteredItemCount.value / itemsPerPage.value);

  pageNumbers.value = [];
  for (let i = 1; i <= Math.ceil(filteredItemCount.value / itemsPerPage.value); i++) {
    console.log(i);
    pageNumbers.value.push(i);
  }

  if (filteredItemCount.value !== 0) {
    displayItems.value = true;
  } else {
    displayError.value = true;
  }
}

const previous = () => {
  if (currentPage.value > 1) {
    currentPage.value = currentPage.value - 1;
  }
};
const next = () => {
  if (currentPage.value >= 1 && currentPage.value < paginateNumber.value) {
    currentPage.value = currentPage.value + 1;
  }
};

watch(currentPage, (currentValue, oldValue) => {
  indexOfLastItem.value = currentValue * itemsPerPage.value;
  indexOfFirstItem.value = indexOfLastItem.value - itemsPerPage.value;
  console.log(indexOfLastItem.value);
  currentItems.value = filteredItems.value.slice(indexOfFirstItem.value, indexOfLastItem.value);
});

const displayError = ref(false);
</script>

<template>
  <h1 class="text-center">Item Search</h1>

  <div class="md:w-2/3 m-auto p-2">
    <p>
      Find the perfect items for your dressups! The search will find items that have the matching
      keyword in the title.
    </p>
  </div>

  <div class="bg-gray-300 p-4 md:w-3/4 md:m-auto rounded mb-2 mx-3">
    <div class="flex flex-col mb-4">
      <form @submit.prevent="searchFor()">
        <label>Enter Item Name:</label>
        <div class="flex gap-2">
          <input type="text" v-model="search" placeholder="" class="px-2 w-2/3" />
          <button class="primary-button w-1/3">Search</button>
        </div>
      </form>
    </div>
  </div>

  <div class="flex flex-col items-center">
    <div v-if="displayError">❗ No items found with this query ❗</div>

    <div v-if="displayItems" class="text-center">Number of items: {{ filteredItemCount }}</div>
    <div v-if="displayItems" class="w-full px-2 md:grid md:grid-cols-3 md:px-24 mb-2">
      <div
        v-for="item in currentItems"
        :key="item.Image"
        class="rounded border flex flex-col h-48 m-1 bg-gray-300 border-black md:border-0"
      >
        <div
          class="text-center font-bold px-1 rounded-t bg-[#227c9d] text-white truncate capitalize"
        >
          <p class="">{{ item.name }}</p>
        </div>
        <div class="grow self-center flex items-stretch">
          <img class="max-h-24 self-center py-2" :src="item.image" />
        </div>
        <a :href="item.archivePage" class="linkButton">View Archive Page</a>
      </div>
    </div>

    <div v-if="displayItems" class="flex justify-center gap-5 mb-2">
      <button class="primary-button w-20" @click="previous">Previous</button>
      <div class="flex items-center gap-1">
        <select
          name="pages"
          id="pages"
          class="p-2 border-2 rounded border-gray-500"
          v-model="currentPage"
        >
          <option
            v-for="(item, itemIndex) in pageNumbers"
            :value="itemIndex + 1"
            :key="itemIndex"
            @click="currentPage = item"
          >
            {{ item }}
          </option>
        </select>
        of {{ pageNumbers[pageNumbers.length - 1] }}
      </div>
      <button class="primary-button w-20" @click="next">Next</button>
    </div>
  </div>
</template>

<style>
.linkButton {
  @apply bg-[#17c3b2] hover:bg-[#0f7a82] text-[#442609] font-bold hover:text-white text-center rounded p-2 m-2;
}
</style>