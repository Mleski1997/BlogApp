const BASE_URL = 'http://localhost:5000/api'

export const fetchPosts = async () => {
	try {
		const response = await fetch(`${BASE_URL}/posts`)
		if (!response.ok) {
			throw new Error('Network response was not ok')
		}
		return await response.json()
	} catch (error) {
		console.error('There was a problem with your fetch operation:', error)
		throw error
	}
}

export const fetchPostById = async id => {
	try {
		const response = await fetch(`${BASE_URL}/posts/${id}`)
		if (!response.ok) {
			throw new Error('Network response was not ok')
		}
		return await response.json()
	} catch (error) {
		console.error('There was a problem with your fetch operation:', error)
		throw error
	}
}
